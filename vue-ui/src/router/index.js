import Vue from 'vue';
import VueRouter from 'vue-router';
import CustomerList from '../views/customer/CustomerList.vue';

Vue.use(VueRouter);

const routes = [
  {
    path: '/customers',
    name: 'customers',
    component: CustomerList
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

export default router;
