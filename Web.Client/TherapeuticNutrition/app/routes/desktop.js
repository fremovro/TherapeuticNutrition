import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class DesktopRoute extends Route {
  @service restApi;

  @action didTransition() {
    var _this = this;

    _this.controller.set('isLoading', true);

    this.restApi
      .sendGetRequest('https://localhost:7253/TherapeuticNutrition/get/pacient')
      .then(
        function (pacient) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.set('pacient', pacient);
            console.log(_this.controller.get('pacient'));
          }, 500);
        },
        function (reason) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.send('redirect', 'authorization');
            _this.set('pacient', null);
          }, 500);
        },
      );
  }
}
