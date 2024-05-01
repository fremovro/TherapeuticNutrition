import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class AllergensRoute extends Route {
  @service restApi;

  @action didTransition() {
    var _this = this;

    _this.controller.set('isLoading', true);
    this.restApi
      .sendGetRequest(
        'https://localhost:7253/TherapeuticNutrition/get/allergens',
      )
      .then(
        function (allergens) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.set('allergens', allergens.sort(function(a,b){
              return a.name.localeCompare(b.name);
            }));

            _this.controller.set('allAllergens', allergens);
            _this.controller.set('favoriteAllergens', 
              allergens.filter((allergen) => allergen.isFavorite)
            );

            if (_this.controller.get('chosenAllergenPrimarykey') != null) {
              _this.controller.set(
                'chosenAllergen',
                allergens.find((allergen) => allergen.primarykey == _this.controller.get('chosenAllergenPrimarykey')),
              );
            }

            if (_this.controller.get('chosenAllergen') == null) {
              _this.controller.set('chosenAllergen', allergens[0]);
            }
            
            console.log(allergens);
          }, 500);
        },
        function (reason) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.send('redirect', 'authorization');
          }, 500);
        },
      );
  }
}
