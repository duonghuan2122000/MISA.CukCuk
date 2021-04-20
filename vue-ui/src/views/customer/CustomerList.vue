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
          <Combobox style="margin-left: 8px" />
          <Combobox style="margin-left: 8px" />
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
          <template slot="head">
            <tr>
              <th>Mã khách hàng</th>
              <th>Họ và tên</th>
              <th>Email</th>
              <th>Số điện thoại</th>
              <th>Ngày sinh</th>
            </tr>
          </template>
          <template slot="body">
            <tr>
              <td>KH001</td>
              <td>Nguyễn Văn A</td>
              <td>nva@gmail.com</td>
              <td>123456789</td>
              <td>15/04/2021</td>
            </tr>
          </template>
        </Table>
      </div>

      <div class="footer">
        <div>Hiển thị 1 - 10/1000 khách hàng</div>
        <Pagination />
        <div>10 khách hàng / trang</div>
      </div>
    </div>
    <CustomerDialog
      :show="isShowCustomerDialog"
      @onClose="closeCustomerDialog"
    />
  </div>
</template>

<script>
import { ref } from "@vue/composition-api";

import Button from "../../components/Button.vue";
import Input from "../../components/Input.vue";
import Combobox from "../../components/Combobox.vue";
import Table from "../../components/Table.vue";
import Pagination from "../../components/Pagination.vue";
import CustomerDialog from "./CustomerDialog.vue";

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
    const isShowCustomerDialog = ref(false);

    const showCustomerDialog = () => {
      isShowCustomerDialog.value = true;
    };

    const closeCustomerDialog = () => {
      isShowCustomerDialog.value = false;
    };

    return { isShowCustomerDialog, showCustomerDialog, closeCustomerDialog };
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