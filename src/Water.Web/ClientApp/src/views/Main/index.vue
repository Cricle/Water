<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import Menu from "./Menu/index.vue";
</script>

<template>
  <div class="Main">
    <header><Menu></Menu></header>
    <main class="content">
      <RouterView></RouterView>
    </main>
  </div>
</template>

<style scoped>
.Main .content{
    padding:10px
}
</style>
