import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'

Vue.config.productionTip = false




router.beforeEach((to, _, next) => {
  const toMeta = {
      allowUnauthenticatedAccess: false,
      stage: 0,
      authorize: 'Consultant',
      ...(to.meta || {})
  }

  if (toMeta.allowUnauthenticatedAccess || store.getters.isAuthorized) {
      if (toMeta.stage > 1) { // must have a valid transaction
          if (store.getters.activeTransaction && store.getters.activeKey === to.params.key) {
              next()
          } else {
              next('/')
          }
      } else {
          next()
      }
  } else { // not authorized or authenticated for accessing the requested page
      next({name: 'login'})
  }
})



new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
