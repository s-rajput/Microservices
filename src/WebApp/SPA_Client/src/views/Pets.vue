
<template>

    <div id="app-instasearch">

        <input type="text" placeholder="Type a name to Search..........  " v-model="nameVal" />
        <hr />
        Filter by:
        <select v-model="genderVal">
            <option v-for="iOption in genderOptions" :key="iOption.value" :value="iOption.value">{{iOption.label}}</option>
        </select>
        <select v-model="cityVal">
            <option v-for="iOption in cityOptions" :key="iOption.value" :value="iOption.value">{{iOption.label}}</option>
        </select>
        <hr />
        <button v-on:click="Filter">Go</button>
        <button v-on:click="Reset">Reset</button>
        <hr />

        <ul>
            <li v-for="item in photoFeed" :key="item.gender">
                {{ item.gender }}
                <ul>
                    <li v-for="p in item.pets" :key="p.name">
                        {{ p.name }} -  {{ p.city }}
                    </li>
                </ul>
            </li>
        </ul>


    </div>



</template>

<script lang='ts'>

    import Vue from 'vue'
    import axios from 'axios' 

    import { PetsData } from '@/models/petsData'

    export default Vue.extend({

        el: '#app-instasearch',

        data() {

            return {
                nameVal: "",
                genderVal: "",
                genderOptions: [
                    {
                        label: "Gender",
                        value: ""
                    },
                    {
                        label: "Male",
                        value: "male"
                    },
                    {
                        label: "Female",
                        value: "female"
                    }
                ],
                cityVal: "",
                cityOptions: [
                    {
                        label: "City",
                        value: ""
                    },
                    {
                        label: "Sydney",
                        value: "sydney"
                    },
                    {
                        label: "Melbourne",
                        value: "melbourne"
                    }
                ],
                photoFeed: [] as PetsData[]
            }
        },

        mounted() {
            axios
                .get('http://localhost:5002/Pets')
                .then(response => {
                    this.photoFeed = response.data;
                })
                .catch(error => console.log(error))
        },
        methods: {
            async Reset() {
                this.genderVal = '';
                this.cityVal = '';
                this.nameVal = '';
                const res = await axios.get(`http://localhost:5002/Pets?name=${this.nameVal}&city=${this.cityVal}&gender=${this.genderVal}`);
                this.photoFeed = res.data;
            },
            async Filter() {
                const res = await axios.get(`http://localhost:5002/Pets?name=${this.nameVal}&city=${this.cityVal}&gender=${this.genderVal}`);
                this.photoFeed = res.data;
            },
            fetchCats() {

                axios
                    .get('http://localhost:5002/Pets')
                    .then(response => {
                        let photos = response.data;
                        //photos = this.photoFeed;
                        const nameVal = this.nameVal;

                        if (!nameVal) {
                            this.photoFeed = photos; return;
                        }

                        photos = photos.filter(function (item) {
                            alert(item.Gender);
                            if (item.Gender.indexOf(nameVal) !== -1) {
                                return item;
                            }
                        })
                        this.photoFeed = photos; return;
                    })
                    .catch(error => console.log(error))

                return this.photoFeed;

            },
        },
        computed: {

           
        }

    })

</script>


<style>



    * {
        box-sizing: border-box;
        font-family: 'Nunito', sans-serif;
    }

    html,
    body {
        height: 100%;
        margin: 0;
        padding: 0;
        width: 100%;
    }

    input-container {
        border-radius: 5px;
        background: #677482;
        padding: 10px;
    }

    .input-container input {
        border: none;
        background: transparent;
        color: white;
        padding: 6px 15px;
        font-size: 18px;
    }

    ::placeholder { /* Chrome, Firefox, Opera, Safari 10.1+ */
        color: #a6b0ba;
        opacity: 1; /* Firefox */
    }

    .photo {
        list-style: none;
        display: grid;
        grid-template-columns: 200px auto;
        margin-top: 20px;
        align-items: center;
        background-color: #e9edf0;
        padding: 30px 50px;
        border-radius: 5px;
        border: 1px solid #e3e3e3;
    }

    .author {
        font-size: 25px;
        margin-left: 20px;
        color: #75818e;
    }

    .photo img {
        border-radius: 5px;
        width: 200px;
    }

    .photo-animated {
        transition: all 0.5s;
    }

    .list-animation-enter, .list-animation-leave-to {
        opacity: 0;
        transform: translateY(30px);
    }

    .list-animation-leave-active {
        position: absolute;
    }
</style>