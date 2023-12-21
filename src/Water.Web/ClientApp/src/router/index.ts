import { createRouter, createWebHistory } from 'vue-router'
import Main from '@/views/Main/index.vue'
import Login from '@/views/Login/index.vue'
import Establishment from '@/views/Establishment/index.vue'
import Activity from '@/views/Activity/index.vue'
import History from '@/views/History/index.vue'
import User from '@/views/User/index.vue'
import Home from '@/views/Home/index.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Main',
      component: Main,
      children: [
        { path: '/', name: 'Home', component: Home },
        {
          path: '/Establishment',
          name: 'Establishment',
          component: Establishment
        },
        {
          path: '/Activity',
          name: 'Activity',
          component: Activity
        },
        {
          path: '/History',
          name: 'History',
          component: History
        },
        {
          path: '/User',
          name: 'User',
          component: User
        },
      ]
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login
    },
  ]
})

router.beforeEach((to,from,next)=>{
  next()
})

export default router
