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

import { TeamDetails } from "./data-contracts";
import { HttpClient, RequestParams } from "./http-client";

export class LeaveCategories<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Leaves
   * @name LeaveCategoriesList
   * @request GET:/leaveCategories
   */
  leaveCategoriesList = (params: RequestParams = {}) =>
    this.request<TeamDetails[], string>({
      path: `/leaveCategories`,
      method: "GET",
      format: "json",
      ...params,
    });
}
