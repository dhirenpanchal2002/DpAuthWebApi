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

export interface DateOnly {
  /** @format int32 */
  year?: number;
  /** @format int32 */
  month?: number;
  /** @format int32 */
  day?: number;
  dayOfWeek?: DayOfWeek;
  /** @format int32 */
  dayOfYear?: number;
  /** @format int32 */
  dayNumber?: number;
}

/** @format int32 */
export enum DayOfWeek {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
  Value4 = 4,
  Value5 = 5,
  Value6 = 6,
}

/** @format int32 */
export enum LeaveCategory {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
}

export enum TodoStatus {
  Pending = 'Pending',
  InProgress = 'InProgress',
  Completed = 'Completed',
}

export interface LeaveDetail {
  id?: string | null;
  userId?: string | null;
  description?: string | null;
  leaveDay?: LeaveCategory;
  startDate?: DateOnly;
  endDate?: DateOnly;
  status?: LeaveStatus;
  type?: LeaveType;
  approver?: UserDetails;
  /** @format date-time */
  approvedAt?: string;
  /** @format date-time */
  createdAt?: string;
}

/** @format int32 */
export enum LeaveStatus {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
}

/** @format int32 */
export enum LeaveType {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
}

export interface RegisterUser {
  firstName?: string | null;
  lastName?: string | null;
  userName?: string | null;
  emailId?: string | null;
  password?: string | null;
}

export interface TeamDetails {
  id?: string | null;
  teamName?: string | null;
  teamLead?: UserDetails;
  teamEmailId?: string | null;
  isDeleted?: boolean;
  /** @format date-time */
  createdAt?: string;
  teamMembers?: UserDetails[] | null;
}

export interface TodoDetail {
  id?: string | null;
  summary?: string | null;
  description?: string | null;
  status?: string | null;
  /** @format date-time */
  completedOn?: string | null;
  /** @format date-time */
  createdAt?: string | null;
}

export interface UpdateUserPassword {
  emailId?: string | null;
  verificationCode?: string | null;
  password?: string | null;
}

export interface UserDetails {
  id?: string | null;
  userName?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  emailId?: string | null;
  photoUrl?: string | null;
  authToken?: string | null;
  isDeleted?: boolean;
  isVerificationCodeSet?: boolean;
  /** @format date-time */
  createdAt?: string;
}

export interface UserLogin {
  password?: string | null;
  userName?: string | null;
  newPassword?: string | null;
  token?: string | null;
}
