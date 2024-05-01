import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class RecipesController extends Controller {
  @service restApi;
  @service router;
  queryParams = ['chosenRecipePrimarykey'];

  isLoading = false;
  favoriteFlag = false; // Избранное: фильтр

  allRecipes = []; // Избранное: фильтр
  favoriteRecipes = []; // Избранное: фильтр
  recipes = [];

  searchStr = '';
  chosenRecipePrimarykey = null;
  chosenRecipe = null;
  recipeImageUrl = 'https://www.ulfven.no/files/sculptor30/library/images/default-product-image.png'

  // Default
  @action redirect(route) {
    this.router.transitionTo(route);
  };
  @action changeChosenRecipe(recipe) {
    this.set('chosenRecipe', recipe);
    this.send('setRecipeImageUrl');
  };
  // Default

  // Избранное
  @action makeFavorite(recipe) {
    this.set('chosenRecipe.isFavorite', !recipe.isFavorite);

    // Избранное: фильтр
    let favoriteRecipes = this.get('favoriteRecipes');
    if (recipe.isFavorite) {
      favoriteRecipes.push(recipe);
      this.set('favoriteRecipes', favoriteRecipes);
    }
    else {
      favoriteRecipes = favoriteRecipes.filter(item => item !== recipe);
      this.set('favoriteRecipes', favoriteRecipes);
    }
    if (this.get('favoriteFlag')) {
      this.set('recipes', favoriteRecipes.sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }

    this.restApi
      .sendPostRequest(
        'pacient/change/type=Recipe&primarykey=' +
        recipe.primarykey +
          '&isFavorite=' +
          recipe.isFavorite,
      )
      .then(
        function (pacient) {
          console.log(pacient);
        },
        function (error) {
          console.log(error);
        }
      );
  };
  @action changeFavoriteFlag() {
    var isFavorite = !this.get('favoriteFlag');
    this.set('favoriteFlag', isFavorite);

    if (isFavorite) {
      this.set('recipes', this.get('favoriteRecipes').sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }
    else {
      this.set('recipes', this.get('allRecipes').sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }

    if (this.get('searchStr')) {
      var searchRecipes = this.get('recipes')
        .filter(item => item.name.toLowerCase().includes(this.get('searchStr').toLowerCase()))
        .sort(function(a,b) {return a.name.localeCompare(b.name); });
        this.set('recipes', searchRecipes);
    }

    this.send('changeChosenRecipe', this.get('recipes')[0]);
  };
  // Избранное

  // Поиск
  @action changeSearchStr() {
    setTimeout(() => {
      var searchStr = this.get('searchStr');
      if (this.get('searchStr')) {
        if (this.get('favoriteFlag')) {
          var searchRecipes = this.get('favoriteRecipes')
            .filter(item => item.name.toLowerCase().includes(searchStr.toLowerCase()))
            .sort(function(a,b) {return a.name.localeCompare(b.name); });
            this.set('recipes', searchRecipes);
        }
        else {
          var searchRecipes = this.get('allRecipes')
            .filter(item => item.name.toLowerCase().includes(searchStr.toLowerCase()))
            .sort(function(a,b) {return a.name.localeCompare(b.name); });
            this.set('recipes', searchRecipes);
        }
      }
      else {
        if (this.get('favoriteFlag')) {
          this.set('recipes', this.get('favoriteRecipes'));
        }
        else {
          this.set('recipes', this.get('allRecipes'));
        }
      }
    }, 250);
  };
  // Поиск

  // Image
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
  // Image
}
