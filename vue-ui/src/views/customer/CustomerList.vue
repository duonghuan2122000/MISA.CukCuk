<template>
  <div>
    <div v-if="!isLoading && !isSuccess" class="content error">
      Error. Vui lòng thử lại.
    </div>
    <div v-else class="content">
      <div class="title-box">
        <div class="title">DANH SÁCH KHÁCH HÀNG</div>
        <Button
          text="Thêm khách hàng"
          iconLeft="user-plus"
          @click="setStateCustomerDialog(true)"
        />
      </div>

      <div class="toolbar-box" style="margin-top: 16px">
        <div class="toolbar-left">
          <Input
            placeholder="Nhập mã, họ tên hoặc số điện thoại khách hàng"
            style="width: 300px"
            v-model="customerFilter"
          />
          <Combobox
            style="margin-left: 8px; width: 150px"
            :options="customerGroupOptions"
            v-model="selectedCustomerGroupId"
          />
        </div>
        <div class="toolbar-right">
          <Button
            :color="null"
            :onlyIcon="true"
            icon="sync"
            @click="refreshData"
          />
          <Button
            style="margin-left: 8px"
            styleBtn="background-color: #f20"
            styleIcon="color: #fff"
            :color="null"
            :onlyIcon="true"
            icon="trash"
            @click="onClickDeleteCustomer"
          />
        </div>
      </div>

      <div class="scroll">
        <div v-if="isLoading" class="loading">
          <div class="loader"></div>
        </div>
        <div v-else-if="isSuccess">
          <Table v-if="customers.length > 0">
            <template v-slot:head>
              <tr>
                <th>Mã khách hàng</th>
                <th>Họ và tên</th>
                <th>Ngày sinh</th>
                <th>Giới tính</th>
                <th>Nhóm khách hàng</th>
                <th>Email</th>
                <th>Số điện thoại</th>
              </tr>
            </template>
            <template v-slot:body>
              <tr
                v-for="c in customers"
                :key="c.customerId"
                :class="{
                  selected:
                    selectedCustomerDel &&
                    selectedCustomerDel.customerId == c.customerId,
                }"
                @click="onSelectCustomer(c)"
              >
                <td>{{ c.customerCode }}</td>
                <td>{{ c.fullName }}</td>
                <td>{{ formatDate(c.dateOfBirth) }}</td>
                <td>{{ c.genderName }}</td>
                <td>{{ c.customerGroupName }}</td>
                <td>{{ c.email }}</td>
                <td>{{ c.phoneNumber }}</td>
              </tr>
            </template>
          </Table>
          <div v-else>Không có dữ liệu</div>
        </div>
      </div>

      <div class="footer">
        <div>
          Hiển thị {{ startRecord }} - {{ endRecord }}/{{ totalRecord }} khách
          hàng
        </div>
        <Pagination :page="page" :totalPages="totalPages" />
        <div>{{ pageSize }} khách hàng / trang</div>
      </div>
    </div>
    <CustomerDialog
      :show="isShowCustomerDialog"
      v-model:customer="customerModify"
      :customerGroupOptions="customerGroupOptions.slice(1)"
      @onChange="setStateCustomerDialog"
    />
    <ConfirmDialog
      :show="isShowConfirmDialog"
      :message="msgConfirmDialog"
      @onChange="setStateConfirmDialog"
      @onOk="delCustomer"
      @onCancel="cancelDelCustomer"
    />
    <AlertDialog
      :show="isShowAlertDialog"
      :message="msgAlertDialog"
      @onChange="setStateAlertDialog"
    />
  </div>
</template>

<script>
import { ref, computed, onMounted, watch } from "vue";
import { useRoute, useRouter } from "vue-router";

import Button from "../../components/Button.vue";
import Input from "../../components/Input.vue";
import Combobox from "../../components/Combobox.vue";
import Table from "../../components/Table.vue";
import Pagination from "../../components/Pagination.vue";
import ConfirmDialog from "../../components/ConfirmDialog.vue";
import CustomerDialog from "./CustomerDialog.vue";
import AlertDialog from "../../components/AlertDialog.vue";

import StateEnum from "../../store/StateEnum";

import axios from "../../helpers/axios";

export default {
  components: {
    Button,
    Input,
    Combobox,
    Table,
    Pagination,
    ConfirmDialog,
    CustomerDialog,
    AlertDialog,
  },
  setup() {
    const state = ref(StateEnum.LOADING);

    /**
     * Danh sách khách hàng.
     */
    const customers = ref([]);

    /**
     * Tổng số trang.
     */
    const totalPages = ref(0);

    /**
     * Tổng số khách hàng.
     */
    const totalRecord = ref(0);

    /**
     * trang hiện tại.
     */
    const page = ref(1);

    /**
     * Số khách hàng trên một trang.
     */
    const pageSize = ref(10);

    /**
     * Thứ tự khách hàng bắt đầu.
     */
    const startRecord = computed(() => (page.value - 1) * pageSize.value + 1);

    /**
     * Thứ tự khách hàng kết thúc.
     */
    const endRecord = computed(() => startRecord.value + pageSize.value - 1);

    /**
     * Danh sách nhóm khách hàng.
     */
    const customerGroupOptions = ref([]);

    /**
     * id nhóm khách hàng đang được chọn ở combobox nhóm khách hàng.
     */
    const selectedCustomerGroupId = ref("");

    /**
     * Từ khóa filter danh sách khách hàng theo tên hoặc số điện thoại.
     */
    const customerFilter = ref("");

    const selectedCustomerDel = ref(null);

    const customerModify = ref(null);

    /**
     * Route
     */
    const route = useRoute();

    const router = useRouter();

    /**
     * Trạng thái trang web.
     */
    const isLoading = computed(() => state.value == StateEnum.LOADING);
    const isSuccess = computed(() => state.value == StateEnum.SUCCESS);

    /**
     * Khởi tạo data.
     */
    const initialData = () => {
      if (route.query && route.query.page) {
        page.value = parseInt(route.query.page);
      }
    };

    /**
     * Hàm refresh dữ liệu.
     */
    const refreshData = () => {
      selectedCustomerGroupId.value = "";
      router.push({ query: { page: 1 } });
    };

    /**
     * Hàm lấy danh sách khách hàng từ api.
     */
    const fetchCustomers = () => {
      state.value = StateEnum.LOADING;
      axios
        .get(
          `/api/v1/customers?page=${page.value}&pageSize=${pageSize.value}&filter=${customerFilter.value}&customerGroupId=${selectedCustomerGroupId.value}`
        )
        .then((res) => res.data)
        .then((data) => {
          totalPages.value = data.totalPages || 0;
          totalRecord.value = data.totalRecord || 0;
          customers.value = data.data || [];
          state.value = StateEnum.SUCCESS;
        })
        .catch((err) => {
          console.log(err);
          state.value = StateEnum.ERROR;
        });
    };

    const fetchCustomerGroups = () => {
      axios
        .get("/api/v1/customer-groups")
        .then((res) => res.data)
        .then((data) => {
          let options = [{ value: "", text: "Nhóm khách hàng" }];
          for (let cg of data) {
            options.push({
              value: cg.customerGroupId,
              text: cg.customerGroupName,
            });
          }
          customerGroupOptions.value = options;
        });
    };

    /**
     * Hook thực hiện khi đã load xong component.
     */
    onMounted(() => {
      initialData();
      fetchCustomers();
      fetchCustomerGroups();
    });

    /**
     * Biến xác định trạng thái dialog khách hàng.
     * CreatedBy: dbhuan (20/04/2021)
     */
    const isShowCustomerDialog = ref(false);

    /**
     * Hàm set trạng thái dialog khách hàng.
     * CreatedBy: dbhuan (20/04/2021)
     */
    const setStateCustomerDialog = (state) => {
      isShowCustomerDialog.value = state;
    };

    /**
     * Lời nhắn cần hiển thị dialog confirm.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const msgConfirmDialog = ref("");

    /**
     * Biến xác định trạng thái dialog confirm.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const isShowConfirmDialog = ref(false);

    /**
     * Hàm set trạng thái dialog confirm.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const setStateConfirmDialog = (state) => {
      isShowConfirmDialog.value = state;
    };

    /**
     * Lời nhắn hiển thị trên dialog alert.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const msgAlertDialog = ref("");

    /**
     * Biến xác định trạng thái dialog alert.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const isShowAlertDialog = ref(false);

    /**
     * Hàm set trạng thái dialog alert.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const setStateAlertDialog = (state) => {
      isShowAlertDialog.value = state;
    };

    /**
     * Hàm click chọn một khách hàng trên bảng.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const onSelectCustomer = (customer) => {
      if (
        !selectedCustomerDel.value ||
        customer.customerId != selectedCustomerDel.value.customerId
      ) {
        selectedCustomerDel.value = customer;
      } else {
        selectedCustomerDel.value = null;
      }
    };

    /**
     * Hàm click button xóa khách hàng.
     * CreatedBy: dbhuan (27/04/2021)
     */
    const onClickDeleteCustomer = () => {
      if (selectedCustomerDel.value == null) {
        // nếu khách hàng cần xóa chưa được chọn thì hiển thị dialog alert.
        msgAlertDialog.value =
          "Bạn phải chọn khách hàng cần xóa trước khi chọn nút xóa.";
        setStateAlertDialog(true);
        return;
      }
      // Hiển thị dialog confirm xác nhận xóa.
      msgConfirmDialog.value = `Bạn có chắc muốn xóa khách hàng [${selectedCustomerDel.value.customerCode}] này khỏi hệ thống không ?`;
      setStateConfirmDialog(true);
    };

    /**
     * Hàm xóa khách hàng.
     */
    const delCustomer = () => {
      // Ẩn dialog confirm và set lời nhắn confirm là "".
      setStateConfirmDialog(false);
      msgConfirmDialog.value = "";

      // call api xóa khách hàng.
      axios
        .delete(`/api/v1/customers/${selectedCustomerDel.value.customerId}`)
        .then((res) => res.data)
        .then(() => {
          // thành công thì hiển thị thông báo thành công trên dialog alert.
          fetchCustomers();
          msgAlertDialog.value = "Xóa thành công.";
          setStateAlertDialog(true);
        })
        .catch(() => {
          // thất bại thì hiển thị thông báo thất bại trên dialog alert.
          msgAlertDialog.value = "Xóa thất bại.";
          setStateAlertDialog(true);
        })
        // thực hiện reload lại dữ liệu sau khi call api.
        .finally(() => cancelDelCustomer());
    };

    /**
     * Hàm hủy xóa khách hàng.
     */
    const cancelDelCustomer = () => {
      setStateConfirmDialog(false);
      msgConfirmDialog.value = "";
      selectedCustomerDel.value = null;
    };

    /**
     * Hàm format date về dạng DD-MM-YYYY.
     * CreatedBy: dbhuan (20/04/2021)
     */
    const formatDate = (dateStr) => {
      let date = new Date(dateStr);
      let dateString =
        date.getDate() < 10 ? "0" + date.getDate().toString() : date.getDate();
      let monthString =
        date.getMonth() < 9
          ? "0" + (date.getMonth() + 1).toString()
          : (date.getMonth() + 1).toString();
      let yearString = date.getFullYear().toString();
      return `${dateString}-${monthString}-${yearString}`;
    };

    /**
     * Hàm theo dõi sự thay đổi của bảng chọn nhóm khách hàng và trang trên router.
     * CreatedBy: dbhuan (27/04/2021)
     */
    watch([selectedCustomerGroupId, () => route.query], () => {
      // Thực hiện load lại dữ liệu khi có sự thay đổi.
      initialData();
      fetchCustomers();
    });

    /**
     * thiết lập một debounce.
     */
    let timeOut = null;
    /**
     * Hàm theo dõi sự thay đổi của input search khách hàng.
     * CreatedBy: dbhuan (27/04/2021)
     */
    watch(customerFilter, () => {
      // thực hiện debounce để lấy dữ liệu cuối cùng mà người dùng nhập.
      clearTimeout(timeOut);

      timeOut = setTimeout(() => {
        // load dữ liệu theo từ khóa tương ứng.
        initialData();
        fetchCustomers();
      }, 1000);
    });

    return {
      customers,
      totalRecord,
      totalPages,
      isLoading,
      isSuccess,
      refreshData,
      selectedCustomerDel,
      page,
      pageSize,
      startRecord,
      endRecord,
      customerModify,
      selectedCustomerGroupId,
      customerFilter,
      isShowCustomerDialog,
      setStateCustomerDialog,
      isShowConfirmDialog,
      msgConfirmDialog,
      setStateConfirmDialog,
      isShowAlertDialog,
      msgAlertDialog,
      setStateAlertDialog,
      onSelectCustomer,
      onClickDeleteCustomer,
      delCustomer,
      cancelDelCustomer,
      formatDate,
      customerGroupOptions,
    };
  },
};
</script>

<style>
.content {
  position: absolute;
  top: 56px;
  left: 226px;
  right: 0;
  bottom: 0;
  padding: 16px;
  display: flex;
  flex-direction: column;
}

.content .title-box {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.content .title-box .title {
  font-size: 20px;
  font-weight: bold;
}

.content .toolbar-box {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.content .toolbar-left {
  display: flex;
  flex-direction: row;
}

.content .scroll {
  flex: 1;
  margin-top: 16px;
  margin-bottom: 16px;
  overflow: auto;
}

.content .footer {
  height: 56px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  flex-wrap: wrap;
  border-top: 5px solid #ccc;
}

.loading {
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Loader */
.loader {
  border: 2px solid #f3f3f3; /* Light grey */
  border-top: 2px solid green; /* Blue */
  border-radius: 50%;
  width: 30px;
  height: 30px;
  animation: spin 2s linear infinite;
}

.error {
  text-align: center;
  font-size: 15px;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>