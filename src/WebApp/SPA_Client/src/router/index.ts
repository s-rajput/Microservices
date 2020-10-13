import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/About.vue' 
import Pets from '../views/Pets.vue';
Vue.use(VueRouter)

  const routes: Array<RouteConfig> = [
   // home redirection
  { path: '/', redirect: '/Pets' },
  {
    path: '/',
    name: 'Home',
    component: Home
      }, 

  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
      }, 
      {
          path: '/Pets',
          component: Pets,
          name: 'Pets',
          meta: { allowUnauthenticatedAccess: true }
      },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
