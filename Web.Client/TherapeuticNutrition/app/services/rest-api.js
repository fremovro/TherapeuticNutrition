import Service from '@ember/service';

export default class RestApiService extends Service {
  baseUrl = 'https://localhost:7253/TherapeuticNutrition/';

  sendGetRequest = function (url) {
    return new Promise(function (resolve, reject) {
      let xhr = new XMLHttpRequest();

      xhr.open('GET', url);
      xhr.onreadystatechange = handler;
      xhr.responseType = 'json';
      xhr.setRequestHeader('Accept', 'application/json');
      xhr.withCredentials = true;
      xhr.send();

      function handler() {
        if (this.readyState === this.DONE) {
          if (this.status === 200) {
            resolve(this.response);
          } else {
            reject(
              new Error(
                'getJSON: `' +
                  url +
                  '` failed with status: [' +
                  this.status +
                  ']',
              ),
            );
          }
        }
      }
    });
  };

  sendPostRequest = function (url) {
    var _this = this;

    return new Promise(function (resolve, reject) {
      let xhr = new XMLHttpRequest();

      var temp = _this.baseUrl + url;
      xhr.open('POST', temp);
      xhr.onreadystatechange = handler;
      xhr.responseType = 'json';
      xhr.setRequestHeader('Accept', 'application/json');
      xhr.withCredentials = true;
      xhr.send();

      function handler() {
        if (this.readyState === this.DONE) {
          if (this.status === 200) {
            resolve(this.response);
          } else {
            reject(
              new Error(
                'getJSON: `' +
                  url +
                  '` failed with status: [' +
                  this.status +
                  ']',
              ),
            );
          }
        }
      }
    });
  };
}
