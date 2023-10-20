import {CurrentUser}  from './../providers/AuthContext/Type';

const USER_LOCAL_STORAGE_KEY = 'STARHR-USER';

export function saveUser(user: CurrentUser): void {
  localStorage.setItem(USER_LOCAL_STORAGE_KEY, JSON.stringify(user));
}

export function getUser(): CurrentUser | undefined {
  const user = localStorage.getItem(USER_LOCAL_STORAGE_KEY);
  console.log('Getting storage user : ', user ? JSON.parse(user) : undefined);
  return user ? JSON.parse(user) : undefined;
}

export function removeUser(): void {
  localStorage.removeItem(USER_LOCAL_STORAGE_KEY);
}