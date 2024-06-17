import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  let userInfoMedia: any = localStorage.getItem('userInfoMedia');
  if (!!userInfoMedia) {
    userInfoMedia = JSON.parse(userInfoMedia);
  }

  if (!!userInfoMedia?.username?.trim()
    || !!userInfoMedia?.email?.trim()
    || !!userInfoMedia?.id?.trim()) {
    return true;
  }
  else {
    router.navigate(['/login']);
    return false;
  }

};
