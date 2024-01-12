<script setup lang="ts">
import { reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { useRouter } from 'vue-router'

const ruleFormRef = ref<FormInstance>();
const router = useRouter()

const ruleForm = reactive({
  password: "",
  userName: "",
});

const rules = reactive<FormRules<typeof ruleForm>>({
  userName: [{ message: "请输入用户名", required: true }],
  password: [{ message: "请输入密码", required: true }],
});

const submitForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  formEl.validate((valid) => {
    if (valid) {
      console.log("submit!");
      router.push({path:'/'})
    } else {
      console.log("error submit!");
      return false;
    }
  });
};
</script>

<template>
  <div class="login flex">
    <!-- <el-image fit="cover" class="login-img" :src=loginBg></el-image> -->
    <div class="login-content">
      <div class="content-left flex">
        <p class="title">Welcome!</p>
        <p class="title1">Sign in to your Account</p>
      </div>
      <div class="content-right flex">
        <el-form
          class="login-form"
          ref="ruleFormRef"
          status-icon
          :rules="rules"
          :model="ruleForm"
          label-width="120px"
        >
          <el-form-item label="UserName" prop="userName">
            <el-input v-model="ruleForm.userName" autocomplete="off" />
          </el-form-item>
          <el-form-item label="Password" prop="password">
            <el-input
              v-model="ruleForm.password"
              type="password"
              autocomplete="off"
            />
          </el-form-item>
          <el-form-item class="submit-btn">
            <!-- <el-button @click="resetForm(ruleFormRef)">清空</el-button> -->
            <el-button
              class="btn"
              type="primary"
              @click="submitForm(ruleFormRef)"
              >登录</el-button
            >
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.flex {
  display: flex;
  align-items: center;
  justify-content: center;
}
.login {
  overflow: hidden;
  height: 100%;
  width: 100%;
  min-width: 900px;
  min-height: 500px;
  background-image: url("../../assets/svg/login-bg.svg");
  background-size: 100%;
  background-repeat: no-repeat;
  background-position: bottom;

  .login-content {
    background-color: rgba($color: #fff, $alpha: 0.8);
    width: 800px;
    height: 500px;
    box-shadow: 0 0 10px 0 #eee;
    border-radius: 4px;
    display: flex;
    // position: relative;
    background-image: url("../../assets/svg/login-bg1.svg");
    background-size: 100%;
    background-repeat: no-repeat;
    background-position: top;
    .content-left {
      width: 200px;
      flex-direction: column;
      height: 100%;
      .title {
        font-size: 20px;
        color: #0aa3ef;
        font-weight: bold;
      }
      .title1 {
        color: #0aa3ef9d;
      }
    }
    .content-right {
      width: 600px;
      .login-form {
        width: 90%;
        padding-top: 80px;
      }
    }
  }
}
:deep(.submit-btn) {
  .el-form-item__content {
    justify-content: center;
  }

  .btn{
    margin-top: 20px;
    width: 70%;
  }
}
</style>
