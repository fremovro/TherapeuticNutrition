import Ember from 'ember';
import Controller from '@ember/controller';
import { computed } from '@ember/object';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class AuthorizationController extends Controller {
  @service restApi;
  @service router;

  login = null;
  password = null;

  @computed('login.length', 'password.length') get disabledLoginButton() {
    if (
      this.login &&
      this.login.length > 4 &&
      this.password &&
      this.password.length > 4
    ) {
      return false;
    }
    return true;
  }

  @action redirect(route) {
    this.router.transitionTo(route);
  }

  @action authorization() {
    var _this = this;

    this.restApi.sendGetRequest('https://localhost:7253/TherapeuticNutrition/login/login=' + this.login + '&password=' + this.password)
      .then(
        function (json) {
          _this.redirect('desktop');
        },
        function (reason) {
          // _this.controller.send('redirect', 'authorization');
        }
      );
  }
}
