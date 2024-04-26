import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class AllergensRoute extends Route {
  @service restApi;

  @action didTransition() {
    var _this = this;

    // this.restApi.sendGetRequest('https://localhost:7253/TherapeuticNutrition/get/pacient')
    //     .then(
    //     function (pacient) {
    //         _this.controller.set('pacient', pacient);
    //         console.log(_this.controller.get('pacient'));
    //     },
    //     function (reason) {
    //         _this.set('pacient', null);
    //     },
    // );
    // const login = document.cookie.split(";").find((result) => {
    //     result.split('=')[0] === 'login'
    // }).split('=')[1];
    // onsole.log(login)
    this.restApi.sendGetRequest('https://localhost:7253/TherapeuticNutrition/get/allergens')
        .then(
        function (allergens) {
            console.log(allergens);
        },
        function (reason) {
            
        },
    );
  }
}
