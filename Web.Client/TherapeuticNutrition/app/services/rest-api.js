import Service from '@ember/service';

export default class RestApiService extends Service {
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
}
