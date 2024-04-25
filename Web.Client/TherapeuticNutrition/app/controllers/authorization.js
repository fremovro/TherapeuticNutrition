import Ember from 'ember';
import Controller from '@ember/controller';
import { computed } from '@ember/object';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class AuthorizationController extends Controller {
  @service proxyService;
  @service router;

  login = null;
  password = null;

  @computed('login', 'password') get disabledLoginButton() {
    if ((this.login && this.login.length > 4) && (this.password && this.password.length > 4)) {
      return false;
    }
    return true;
  };

  @action redirect(route) {
    this.router.transitionTo(route);
  }

  @action authorization() {
    var _this = this;

    this.proxyService.sendGetRequest(
      'https://localhost:7253/TherapeuticNutrition/login/login=' + this.login +'&password=' + this.password,
    ).then(
      function (json) {
        _this.redirect('desktop');
      },
      function (reason) {
        // _this.controller.send('redirect', 'authorization');
      },
    );
  }
}
