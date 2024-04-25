import Ember from 'ember';
import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';

export default class DesktopRoute extends Route {
  @service proxyService;

  init() {
    super.init(...arguments);
    console.log(this.proxyService.getJSON);
    this.proxyService.sendGetRequest(
      'https://localhost:7253/TherapeuticNutrition/get/pacient',
    ).then(
      function (json) {
        console.log(json);
      },
      function (reason) {
        // _this.controller.send('redirect', 'authorization');
      },
    );
  }
}
