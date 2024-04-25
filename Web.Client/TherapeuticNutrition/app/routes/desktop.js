import Ember from 'ember';
import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class DesktopRoute extends Route {
  @service restApi;

  @action didTransition() {
    var _this = this;

    this.restApi.sendGetRequest('https://localhost:7253/TherapeuticNutrition/get/pacient')
      .then(
        function (pacient) {
          _this.controller.set('pacient', pacient);
        },
        function (reason) {
          _this.set('pacient', null);
        }
      );
  }
}
