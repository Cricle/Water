<script setup lang="ts">
import HomeIcon from '@/components/icons/HomeIcon.vue'
import AddIcon from '@/components/icons/AddIcon.vue';
import UserIcon from '@/components/icons/UserIcon.vue';
import { ref } from 'vue'
import {useCounterStore } from '@/stores/counter'

const activeIndex = ref('1')
const handleSelect = (key: string, keyPath: string[]) => {
    console.log(key, keyPath)
}
useCounterStore().increment();
console.log(useCounterStore().count);
</script>
<template>
    <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" :ellipsis="false">
        <el-menu-item index="0">
            <HomeIcon></HomeIcon>
        </el-menu-item>
        <el-menu-item index="1">
            <AddIcon></AddIcon>
        </el-menu-item>
        <div class="flex-grow"></div>
        <el-menu-item index="1">
            <AddIcon></AddIcon>
        </el-menu-item>
    </el-menu>
</template>

<style scoped>
.flex-grow {
    flex-grow: 1;
}
</style>