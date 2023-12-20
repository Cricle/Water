import { defineStore } from 'pinia'
import { ref } from 'vue'
export interface UserState{
    isLogin:boolean;
    userName:string;
    userId:string;
    userAvatar:string;
}
export class UserManager{

}