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
  recipeImageUrl = 'https://www.ulfven.no/files/sculptor30/library/images/default-product-image.png'

  @action redirect(route) {
    this.router.transitionTo(route);
  };

  @action changeChosenRecipe(recipe) {
    this.set('chosenRecipe', recipe);
    this.send('setRecipeImageUrl');
  };

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
  };

  @action setRecipeImageUrl() {
    var _this = this;

    var chosenRecipe = _this.get('chosenRecipe');
    if (!chosenRecipe) return null;

    this.restApi.sendGetRequest('https://localhost:7253/TherapeuticNutrition/get/image/relation=' + chosenRecipe.primarykey)
    .then(
      function (recipeImageUrl) {
        _this.set('recipeImageUrl', recipeImageUrl);
      },
      function (reason) {
        _this.set('recipeImageUrl', 'https://www.ulfven.no/files/sculptor30/library/images/default-product-image.png');
      }
    );
  };
}
