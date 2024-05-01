import Controller from '@ember/controller';
import { action } from '@ember/object';
import { computed, set } from '@ember/object';
import { inject as service } from '@ember/service';

export default class AllergensController extends Controller {
  @service restApi;
  @service router;
  queryParams = ['chosenAllergenPrimarykey'];

  isLoading = false;
  favoriteFlag = false; // Избранное: фильтр

  allAllergens = []; // Избранное: фильтр
  favoriteAllergens = []; // Избранное: фильтр
  allergens = [];

  searchStr = ''; // Поиск: фильтр
  chosenAllergenPrimarykey = null;
  chosenAllergen = null;

  // Default
  @action redirect(route) {
    this.router.transitionTo(route);
  };
  @action changeChosenAllergen(allergen) {
    this.set('chosenAllergen', allergen);
  };
  // Default

  // Шкала
  calculateDegree = function(position){
    var degree = this.get('chosenAllergen.dangerDegree');

    if (Math.floor(degree/position) >= 1){
      return 100;
    }
    if (degree > position){
      return (degree % position) * 100;
    }
    return 0;
  };
  @computed('chosenAllergen') 
    get dangerDegrees() {
      let result = [];
      for (var i = 1; i <= 5; i++) {
        var calculateDegree =  this.calculateDegree(i);
        if (calculateDegree == 0) {
          result.push("background: #deba89");
          continue;
        }
        result.push("background: linear-gradient( to right, #6d993a 0%, #6d993a " + calculateDegree + 
          "%, #deba89 " + (100 - calculateDegree) + "%, #deba89 100% );");
      }
      return  result;
    }
  // Шкала

  // Избранное
  @action makeFavorite(allergen) {
    this.set('chosenAllergen.isFavorite', !allergen.isFavorite);

    // Избранное: фильтр
    let favoriteAllergens = this.get('favoriteAllergens');
    if (allergen.isFavorite) {
      favoriteAllergens.push(allergen);
      this.set('favoriteAllergens', favoriteAllergens);
    }
    else {
      favoriteAllergens = favoriteAllergens.filter(item => item !== allergen);
      this.set('favoriteAllergens', favoriteAllergens);
    }
    if (this.get('favoriteFlag')) {
      this.set('allergens', favoriteAllergens.sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }

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
        }
      );
  };
  @action changeFavoriteFlag() {
    var isFavorite = !this.get('favoriteFlag');
    this.set('favoriteFlag', isFavorite);

    if (isFavorite) {
      this.set('allergens', this.get('favoriteAllergens').sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }
    else {
      this.set('allergens', this.get('allAllergens').sort(function(a,b){
        return a.name.localeCompare(b.name);
      }));
    }

    if (this.get('searchStr')) {
      var searchAllergens = this.get('allergens')
        .filter(item => item.name.toLowerCase().includes(this.get('searchStr').toLowerCase()))
        .sort(function(a,b) {return a.name.localeCompare(b.name); });
        this.set('allergens', searchAllergens);
    }

    this.send('changeChosenAllergen', this.get('allergens')[0]);
  };
  // Избранное

  // Поиск
  @action changeSearchStr() {
    setTimeout(() => {
      var searchStr = this.get('searchStr');
      if (this.get('searchStr')) {
        if (this.get('favoriteFlag')) {
          var searchAllergens = this.get('favoriteAllergens')
            .filter(item => item.name.toLowerCase().includes(searchStr.toLowerCase()))
            .sort(function(a,b) {return a.name.localeCompare(b.name); });
            this.set('allergens', searchAllergens);
        }
        else {
          var searchAllergens = this.get('allAllergens')
            .filter(item => item.name.toLowerCase().includes(searchStr.toLowerCase()))
            .sort(function(a,b) {return a.name.localeCompare(b.name); });
            this.set('allergens', searchAllergens);
        }
      }
      else {
        if (this.get('favoriteFlag')) {
          this.set('allergens', this.get('favoriteAllergens'));
        }
        else {
          this.set('allergens', this.get('allAllergens'));
        }
      }
    }, 250);
  };
  // Поиск
}