<template>
  <div>
    <div class="content">
      <div class="title-box">
        <div class="title">DANH SÁCH KHÁCH HÀNG</div>
        <Button
          text="Thêm khách hàng"
          iconLeft="user-plus"
          @click="showCustomerDialog"
        />
      </div>

      <div class="toolbar-box" style="margin-top: 16px">
        <div class="toolbar-left">
          <Input />
          <Combobox style="margin-left: 8px" :options="customerGroupOptions" />
        </div>
        <div class="toolbar-right">
          <Button :color="null" :onlyIcon="true" icon="sync" />
          <Button
            style="margin-left: 8px"
            :color="null"
            :onlyIcon="true"
            icon="trash"
          />
        </div>
      </div>

      <div class="scroll">
        <Table>
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
            <tr v-for="c in customers" :key="c.customerId">
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
      @onClose="closeCustomerDialog"
    />
  </div>
</template>

<script>
import { ref, computed, onMounted } from "vue";
// import { useRoute } from "vue-router";

import Button from "../../components/Button.vue";
import Input from "../../components/Input.vue";
import Combobox from "../../components/Combobox.vue";
import Table from "../../components/Table.vue";
import Pagination from "../../components/Pagination.vue";
import CustomerDialog from "./CustomerDialog.vue";

import axios from "axios";

export default {
  components: {
    Button,
    Input,
    Combobox,
    Table,
    Pagination,
    CustomerDialog,
  },
  setup() {
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
    const pageSize = ref(20);

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
     * Route
     */
    // const route = useRoute();

    /**
     * Hàm lấy danh sách khách hàng từ api.
     */
    const fetchCustomers = () => {
      axios
        .get(
          `https://localhost:44378/api/v1/customers?page=${page.value}&pageSize=${pageSize.value}`
        )
        .then((res) => res.data)
        .then((data) => {
          totalPages.value = data.totalPages;
          totalRecord.value = data.totalRecord;
          customers.value = data.data;
        })
        .catch((err) => console.log(err));
    };

    const fetchCustomerGroups = () => {
      axios
        .get("https://localhost:44378/api/v1/customergroups")
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
      fetchCustomers();
      fetchCustomerGroups();
    });

    /**
     * Biến xác định trạng thái dialog khách hàng.
     * CreatedBy: dbhuan (20/04/2021)
     */
    const isShowCustomerDialog = ref(true);

    /**
     * Hàm hiển thị dialog khách hàng.
     * CreatedBy: dbhuan (20/04/2021)
     */
    const showCustomerDialog = () => {
      isShowCustomerDialog.value = true;
    };

    /**
     * Hàm ẩn dialog khách hàng.
     * CreatedBy: dbhuan (20/04/2021)
     */
    const closeCustomerDialog = () => {
      isShowCustomerDialog.value = false;
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

    return {
      customers,
      totalRecord,
      totalPages,
      page,
      pageSize,
      startRecord,
      endRecord,
      isShowCustomerDialog,
      showCustomerDialog,
      closeCustomerDialog,
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
</style>