<template>
  <div class="about">
    <h1>This is an about page</h1>

        <ul class='nav navbar-nav navbar-right' v-if='isAuthenticated'>
                <li><router-link to='/auth'>
                    Log out {{user.FirstName}} {{user.LastName}} 
                </router-link></li>
            </ul>
            
  </div>
</template>
