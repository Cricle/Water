<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import Menu from './components/Menu.vue';
</script>

<template>
  <header>
    <Menu></Menu>
    <div class="wrapper">

    </div>
  </header>

  <RouterView />
</template>