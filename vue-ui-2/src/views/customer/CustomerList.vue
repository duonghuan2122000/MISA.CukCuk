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
            :value="customerFilter"
            @input="filterCustomers"
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
                <td>{{ c.dateOfBirth | formatDate }}</td>
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
      :customer.sync="customerModify"
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
import Button from "../../components/Button.vue";
import Input from "../../components/Input.vue";
import Combobox from "../../components/Combobox.vue";
import Table from "../../components/Table.vue";
import Pagination from "../../components/Pagination.vue";
import ConfirmDialog from "../../components/ConfirmDialog.vue";
import CustomerDialog from "./CustomerDialog.vue";
import AlertDialog from "../../components/AlertDialog.vue";

import StateEnum from "../../store/StateEnum.js";

import axios from "../../helpers/axios";
import dayjs from "dayjs";

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
  data() {
    return {
      /**
       * Trạng thái trang web.
       * CreatedBy: dbhuan (29/04/2021)
       */
      state: StateEnum.LOADING,

      /**
       * Tổng số trang.
       * CreatedBy: dbhuan (29/04/2021)
       */
      totalPages: 0,

      /**
       * Tổng số khách hàng.
       * CreatedBy: dbhuan (29/04/2021)
       */
      totalRecord: 0,

      /**
       * trang hiện tại.
       * CreatedBy: dbhuan (29/04/2021)
       */
      page: 1,

      /**
       * Số khách hàng trên một trang.
       * CreatedBy: dbhuan (29/04/2021)
       */
      pageSize: 10,

      /**
       * Danh sách nhóm khách hàng.
       * CreatedBy: dbhuan (29/04/2021)
       */
      customerGroupOptions: [],

      /**
       * Id nhóm khách hàng đang được chọn.
       * CreatedBy: dbhuan (29/04/2021)
       */
      selectedCustomerGroupId: "",

      /**
       * Bộ lọc danh sách khách hàng theo họ tên hoặc số điện thoại.
       * CreatedBy: dbhuan (29/04/2021)
       */
      customerFilter: "",

      /**
       * Khách hàng đang được chọn cần xóa.
       * CreatedBy: dbhuan (29/04/2021)
       */
      selectedCustomerDel: null,

      /**
       * Khách hàng đang được thêm mới hoặc chính sửa.
       * CreatedBy: dbhuan (29/04/2021)
       */
      customerModify: null,

      /**
       * Biến xác định trạng thái dialog khách hàng.
       * CreatedBy: dbhuan (2/04/2021)
       */
      isShowCustomerDialog: false,

      /**
       * Biến xác định trạng thái dialog confirm
       * CreatedBy: dbhuan (29/04/2021)
       */
      isShowConfirmDialog: false,

      /**
       * Biến xác định trạng thái dialog alert.
       * CreatedBy: dbhuan (29/04/2021)
       */
      isShowAlertDialog: false,

      /**
       * Lời nhắn cần hiển thị dialog confirm.
       * CreatedBy: dbhuan (29/04/2021)
       */
      msgConfirmDialog: "",

      /**
       * Lời nhắn cần hiển thị dialog alert.
       * CreatedBy: dbhuan (29/04/2021)
       */
      msgAlertDialog: "",

      /**
       * Biến timeout.
       * CreatedBy: dbhuan (29/04/2021)
       */
      timeOut: null,
    };
  },
  computed: {
    /**
     * Trạng thái trang web đang loading.
     * CreatedBy: dbhuan (29/04/2021)
     */
    isLoading: function () {
      return this.state == StateEnum.LOADING;
    },

    /**
     * Trạng thái trang web đã load thành công.
     * CreatedBy: dbhuan (29/04/2021)
     */
    isSuccess: function () {
      return this.state == StateEnum.SUCCESS;
    },

    /**
     * Thứ tự khách hàng bắt đầu.
     * CreatedBy: dbhuan (29/04/2021)
     */
    startRecord: function () {
      return (this.page - 1) * this.pageSize + 1;
    },

    /**
     * Thứ tự khách hàng kết thúc.
     * CreatedBy: dbhuan (29/04/2021)
     */
    endRecord: function () {
      return this.startRecord + this.pageSize - 1;
    },
  },

  mounted() {
    this.initialData();
    this.fetchCustomers();
    this.fetchCustomerGroups();
  },
  methods: {
    /**
     * Khởi tạo data.
     * CreatedBy: dbhuan (29/04/2021)
     */
    initialData() {
      if (this.$route.query && this.$route.query.page) {
        this.page = parseInt(this.$route.query.page);
      }
    },

    /**
     * Hàm refresh dữ liệu.
     * CreatedBy: dbhuan (29/04/2021)
     */
    refreshData() {
      this.selectedCustomerGroupId = "";
      this.$router.push({ query: { page: 1 } });
    },

    /**
     * Hàm lấy danh sách khách hàng từ api.
     * CreatedBy: dbhuan (29/04/2021)
     */
    fetchCustomers() {
      // set trạng thái trang web là loading.
      this.state = StateEnum.LOADING;

      // call api get dữ liệu danh sách khách hàng.
      axios
        .get(
          `/api/v1/customers?page=${this.page}&pageSize=${this.pageSize}&filter=${this.customerFilter}&customerGroupId=${this.selectedCustomerGroupId}`
        )
        .then((res) => res.data)
        .then((data) => {
          // thành công lấy dữ liệu.

          // set dữ liệu vào data.
          this.totalPages = data.totalPages || 0;
          this.totalRecord = data.totalRecord || 0;
          this.customers = data.data || [];

          // set trang thái trang web là success.
          this.state = StateEnum.SUCCESS;
        })
        .catch(() => {
          // có lỗi xảy ra.

          // set trạng thái trang web là error.
          this.state = StateEnum.ERROR;
        });
    },

    /**
     * Hàm lấy danh sách nhóm khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    fetchCustomerGroups() {
      // call api lấy danh sách khách hàng.
      axios
        .get("/api/v1/customer-groups")
        .then((res) => res.data)
        .then((data) => {
          // thành công.
          let options = [{ value: "", text: "Nhóm khách hàng" }];
          for (let cg of data) {
            options.push({
              value: cg.customerGroupId,
              text: cg.customerGroupName,
            });
          }
          this.customerGroupOptions = options;
        })
        .catch(() => {
          this.state = StateEnum.ERROR;
        });
    },

    /**
     * Hàm lọc danh sách khách hàng theo giá trị input nhập vào.
     */
    filterCustomers() {
      console.log("filter");
    },

    /**
     * Hàm set trạng thái dialog khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    setStateCustomerDialog(state) {
      this.isShowCustomerDialog = state;
    },

    /**
     * Hàm set trạng thái dialog confirm.
     * CreatedBy: dbhuan (29/04/2021)
     */
    setStateConfirmDialog(state) {
      this.isShowConfirmDialog = state;
    },

    /**
     * Hàm set trạng thái dialog alert.
     * CreatedBy: dbhuan (29/04/2021)
     */
    setStateAlertDialog(state) {
      this.isShowAlertDialog = state;
    },

    /**
     * Hàm click chọn một khách hàng trên bảng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    onSelectCustomer(customer) {
      if (
        !this.selectedCustomerDel ||
        customer.customerId != this.selectedCustomerDel.customerId
      ) {
        this.selectedCustomerDel = customer;
      } else {
        this.selectedCustomerDel = null;
      }
    },

    /**
     * Hàm click button xóa khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    onClickDeleteCustomer() {
      if (this.selectedCustomerDel == null) {
        // nếu khách hàng cần xóa chưa được chọn thì hiển thị dialog alert.
        this.msgAlertDialog =
          "Bạn phải chọn khách hàng cần xóa trước khi chọn nút xóa.";
        this.setStateAlertDialog(true);
        return;
      }
      // Hiển thị dialog confirm xác nhận xóa.
      this.msgConfirmDialog = `Bạn có chắc muốn xóa khách hàng [${this.selectedCustomerDel.customerCode}] này khỏi hệ thống không ?`;
      this.setStateConfirmDialog(true);
    },

    /**
     * Hàm xóa khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    delCustomer() {
      // Ẩn dialog confirm và set lời nhắn confirm là "".
      this.setStateConfirmDialog(false);
      this.msgConfirmDialog = "";

      // call api xóa khách hàng.
      axios
        .delete(`/api/v1/customers/${this.selectedCustomerDel.customerId}`)
        .then((res) => res.data)
        .then(() => {
          // thành công thì hiển thị thông báo thành công trên dialog alert.
          this.fetchCustomers();
          this.msgAlertDialog = "Xóa thành công.";
          this.setStateAlertDialog(true);
        })
        .catch(() => {
          // thất bại thì hiển thị thông báo thất bại trên dialog alert.
          this.msgAlertDialog = "Xóa thất bại.";
          this.setStateAlertDialog(true);
        })
        // thực hiện reload lại dữ liệu sau khi call api.
        .finally(() => this.cancelDelCustomer());
    },

    /**
     * Hàm hủy xóa khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    cancelDelCustomer() {
      this.setStateConfirmDialog(false);
      this.msgConfirmDialog = "";
      this.selectedCustomerDel = null;
    },
  },

  watch: {
    "$route.query.page": function () {
      // Thực hiện load lại dữ liệu khi có sự thay đổi.
      this.initialData();
      this.fetchCustomers();
    },
  },
  filters: {
    /**
     * Hàm format date về dạng DD/MM/YYYY.
     * CreatedBy: dbhuan (29/04/2021)
     */
    formatDate: function (dateStr) {
      return dayjs(dateStr).format("DD/MM/YYYY");
    },
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