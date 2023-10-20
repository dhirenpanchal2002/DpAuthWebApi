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

export class LeaveTypes<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Leaves
   * @name LeaveTypesList
   * @request GET:/leaveTypes
   */
  leaveTypesList = (params: RequestParams = {}) =>
    this.request<TeamDetails[], string>({
      path: `/leaveTypes`,
      method: "GET",
      format: "json",
      ...params,
    });
}
