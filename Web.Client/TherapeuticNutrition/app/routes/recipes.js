import Route from '@ember/routing/route';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class RecipesRoute extends Route {
    @service restApi;

  @action didTransition() {
    var _this = this;

    _this.controller.set('isLoading', true);
    this.restApi
      .sendGetRequest(
        'https://localhost:7253/TherapeuticNutrition/get/recipes',
      )
      .then(
        function (recipes) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.set('recipes', recipes);

            if (_this.controller.get('chosenRecipePrimarykey') != null) {
              _this.controller.set(
                'chosenRecipe',
                recipes.find((recipe) => {
                  return (
                    recipe.primarykey ==
                    _this.controller.get('chosenRecipePrimarykey')
                  );
                }),
              );
            }

            if (_this.controller.get('chosenRecipe') == null) {
              _this.controller.set('chosenRecipe', recipes[0]);
            }

            console.log(recipes);
          }, 500);
        },
        function (reason) {
          setTimeout(() => {
            _this.controller.set('isLoading', false);
            _this.controller.send('redirect', 'desktop');
          }, 500);
        },
      );
  }
}
