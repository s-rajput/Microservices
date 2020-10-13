<template>
    <div class='navbar navbar-default navbar-fixed-top' style='padding-right: 30px' role='navigation'>
        <div class='container-fluid'>
            <div class='navbar-header'>
                <router-link class='navbar-brand' :title='strwLink' to='/'>
                     Pets UI Portal
                </router-link>
            </div>
           
 
        </div>

    </div>
</template>

<script lang='ts'>
    import Vue from 'vue' 

    import {User} from '@/models/user' 

    export default Vue.extend({
       
        computed: {
            user (): User | undefined { return this.$store.getters.user },
            isAuthenticated (): boolean { return this.$store.getters.isAuthenticated || false }             
        }
    })
</script>
