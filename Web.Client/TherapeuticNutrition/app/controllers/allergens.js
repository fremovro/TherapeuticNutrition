import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class AllergensController extends Controller {
  @service restApi;
  @service router;
  queryParams = ['chosenAllergenPrimarykey'];

  isLoading = false;
  allergens = [];
  chosenAllergenPrimarykey = null;
  chosenAllergen = null;

  @action redirect(route) {
    this.router.transitionTo(route);
  }

  @action changeChosenAllergen(allergen) {
    this.set('chosenAllergen', allergen);
  }

  @action makeFavorite(allergen) {
    this.set('chosenAllergen.isFavorite', !allergen.isFavorite);

    this.restApi
      .sendPostRequest(
        'pacient/change/type=Allergen&primarykey=' +
          allergen.primarykey +
          '&isFavorite=' +
          allergen.isFavorite,
      )
      .then(
        function (pacient) {
          console.log(pacient);
        },
        function (error) {
          console.log(error);
        },
      );
  }
}
