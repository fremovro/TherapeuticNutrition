import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class RecipesController extends Controller {
    @service restApi;
  @service router;
  queryParams = ['chosenRecipePrimarykey'];

  isLoading = false;
  recipes = [];
  chosenRecipePrimarykey = null;
  chosenRecipe = null;

  @action redirect(route) {
    this.router.transitionTo(route);
  }

  @action changeChosenRecipe(recipe) {
    this.set('chosenRecipe', recipe);
  }

  @action makeFavorite(recipe) {
    this.set('chosenRecipe.isFavorite', !recipe.isFavorite);

    this.restApi
      .sendPostRequest(
        'pacient/change/type=Recipe&primarykey=' +
          recipe.primarykey +
          '&isFavorite=' +
          recipe.isFavorite,
      )
      .then(
        function (recipe) {
          console.log(recipe);
        },
        function (error) {
          console.log(error);
        },
      );
  }
}
