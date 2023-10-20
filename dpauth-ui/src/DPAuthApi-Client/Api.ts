/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import { LeaveDetail, RegisterUser, TeamDetails, TodoDetail, UserDetails, UserLogin } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Api<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Auth
   * @name AuthRegisterCreate
   * @request POST:/api/Auth/Register
   * @secure
   */
  authRegisterCreate = (data: RegisterUser, params: RequestParams = {}) =>
    this.request<string, string>({
      path: `/api/Auth/Register`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Auth
   * @name AuthLoginCreate
   * @request POST:/api/Auth/Login
   * @secure
   */
  authLoginCreate = (data: UserLogin, params: RequestParams = {}) =>
    this.request<string, string>({
      path: `/api/Auth/Login`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Auth
   * @name AuthChangePasswordCreate
   * @request POST:/api/Auth/ChangePassword
   * @secure
   */
  authChangePasswordCreate = (data: UserLogin, params: RequestParams = {}) =>
    this.request<void, string>({
      path: `/api/Auth/ChangePassword`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Auth
   * @name AuthSendVerificationCodeCreate
   * @request POST:/api/Auth/SendVerificationCode
   * @secure
   */
  authSendVerificationCodeCreate = (
    query?: {
      emailId?: string;
    },
    params: RequestParams = {},
  ) =>
    this.request<void, string>({
      path: `/api/Auth/SendVerificationCode`,
      method: "POST",
      query: query,
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Leaves
   * @name LeavesDetailsDetail
   * @request GET:/api/Leaves/{leaveId}/details
   * @secure
   */
  leavesDetailsDetail = (leaveId: string, params: RequestParams = {}) =>
    this.request<LeaveDetail, string>({
      path: `/api/Leaves/${leaveId}/details`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Leaves
   * @name LeavesUserLeavesDetail
   * @request GET:/api/Leaves/user/{userId}/leaves
   * @secure
   */
  leavesUserLeavesDetail = (userId: string, params: RequestParams = {}) =>
    this.request<LeaveDetail[], string>({
      path: `/api/Leaves/user/${userId}/leaves`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Leaves
   * @name LeavesApproverLeavesDetail
   * @request GET:/api/Leaves/approver/{approverId}/leaves
   * @secure
   */
  leavesApproverLeavesDetail = (approverId: string, params: RequestParams = {}) =>
    this.request<LeaveDetail[], string>({
      path: `/api/Leaves/approver/${approverId}/leaves`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Leaves
   * @name LeavesLeaveTypesList
   * @request GET:/api/Leaves/leaveTypes
   * @secure
   */
  leavesLeaveTypesList = (params: RequestParams = {}) =>
    this.request<string[], string>({
      path: `/api/Leaves/leaveTypes`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Leaves
   * @name LeavesLeaveStatusesList
   * @request GET:/api/Leaves/leaveStatuses
   * @secure
   */
  leavesLeaveStatusesList = (params: RequestParams = {}) =>
    this.request<string[], string>({
      path: `/api/Leaves/leaveStatuses`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Leaves
   * @name LeavesLeaveCategoriesList
   * @request GET:/api/Leaves/leaveCategories
   * @secure
   */
  leavesLeaveCategoriesList = (params: RequestParams = {}) =>
    this.request<string[], string>({
      path: `/api/Leaves/leaveCategories`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Teams
   * @name TeamsDetailsDetail
   * @request GET:/api/Teams/{teamId}/Details
   * @secure
   */
  teamsDetailsDetail = (teamId: string, params: RequestParams = {}) =>
    this.request<TeamDetails, string>({
      path: `/api/Teams/${teamId}/Details`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Teams
   * @name TeamsList
   * @request GET:/api/Teams
   * @secure
   */
  teamsList = (params: RequestParams = {}) =>
    this.request<TeamDetails[], string>({
      path: `/api/Teams`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Teams
   * @name TeamsCreateCreate
   * @request POST:/api/Teams/Create
   * @secure
   */
  teamsCreateCreate = (data: TeamDetails, params: RequestParams = {}) =>
    this.request<string, string>({
      path: `/api/Teams/Create`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Teams
   * @name TeamsUpdateUpdate
   * @request PUT:/api/Teams/Update/{teamId}
   * @secure
   */
  teamsUpdateUpdate = (teamId: string, data: TeamDetails, params: RequestParams = {}) =>
    this.request<string, string>({
      path: `/api/Teams/Update/${teamId}`,
      method: "PUT",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Todos
   * @name TodosDetailsDetail
   * @request GET:/api/Todos/{todoId}/details
   * @secure
   */
  todosDetailsDetail = (todoId: string, params: RequestParams = {}) =>
    this.request<TodoDetail, string>({
      path: `/api/Todos/${todoId}/details`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Todos
   * @name TodosAddCreate
   * @request POST:/api/Todos/add
   * @secure
   */
  todosAddCreate = (data: TodoDetail, params: RequestParams = {}) =>
    this.request<string, string>({
      path: `/api/Todos/add`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Todos
   * @name TodosUpdateUpdate
   * @request PUT:/api/Todos/update
   * @secure
   */
  todosUpdateUpdate = (data: TodoDetail, params: RequestParams = {}) =>
    this.request<string, string>({
      path: `/api/Todos/update`,
      method: "PUT",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Todos
   * @name TodosTodosList
   * @request GET:/api/Todos/todos
   * @secure
   */
  todosTodosList = (params: RequestParams = {}) =>
    this.request<TodoDetail[], string>({
      path: `/api/Todos/todos`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Todos
   * @name TodosTodoStatusesList
   * @request GET:/api/Todos/todoStatuses
   * @secure
   */
  todosTodoStatusesList = (params: RequestParams = {}) =>
    this.request<string[], string>({
      path: `/api/Todos/todoStatuses`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Users
   * @name UsersUserDetailsList
   * @request GET:/api/Users/UserDetails
   * @secure
   */
  usersUserDetailsList = (
    query?: {
      userId?: string;
    },
    params: RequestParams = {},
  ) =>
    this.request<UserDetails, string>({
      path: `/api/Users/UserDetails`,
      method: "GET",
      query: query,
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Users
   * @name UsersUsersList
   * @request GET:/api/Users/Users
   * @secure
   */
  usersUsersList = (params: RequestParams = {}) =>
    this.request<UserDetails[], string>({
      path: `/api/Users/Users`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Users
   * @name UsersProfilePictureCreate
   * @request POST:/api/Users/ProfilePicture
   * @secure
   */
  usersProfilePictureCreate = (
    data: {
      /** @format binary */
      uploadedFile?: File;
    },
    params: RequestParams = {},
  ) =>
    this.request<string, string>({
      path: `/api/Users/ProfilePicture`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.FormData,
      format: "json",
      ...params,
    });
}
