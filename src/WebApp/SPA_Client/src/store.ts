import Vue from 'vue'
import Vuex, { StoreOptions } from 'vuex' 
Vue.use(Vuex)
export interface RootState { token: true}

const store: StoreOptions<RootState> = {
    strict: true,
    state: { token:true},
    modules: {     
    }
}

export default new Vuex.Store<RootState>(store)