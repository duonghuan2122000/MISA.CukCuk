import { createRouter, createWebHistory } from 'vue-router';
import CustomerList from '../views/customer/CustomerList.vue';


const routes = [
  {
    path: '/customers',
    name: 'customers',
    component: CustomerList
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
