
    function sendmessage() {
        debugger;
        var firebaseConfigs = {

            authDomain: "apnadoctor-4be27.firebaseapp.com",
            databaseURL: "https://apnadoctor-4be27-default-rtdb.firebaseio.com",
            projectId: "apnadoctor-4be27",
            storageBucket: "apnadoctor-4be27.appspot.com",
            messagingSenderId: "724843782490",
            appId: "1:724843782490:web:d38aa433f4006638015363",
            apiKey: "AIzaSyAkAfr7ESRjvbsvzBDj-4uzbd3LcXPPJf8",
            measurementId: "G-4PHBNW84W9"
        };
        firebase.initializeApp(firebaseConfigs).firestore();
        firebase.app().firestore();

        captchaInit = () => {
            if (this.applicationVerifier && this.recaptchaWrapperRef) {
                this.applicationVerifier.clear()
                this.recaptchaWrapperRef.innerHTML = `<div id="recaptcha-container"></div>`
            }


        }

        var ab = '<div id="recaptcha-container"></div>';

        var number = '@ViewBag.Number';
        // var recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container');
        var applicationVerifier = new firebase.auth.RecaptchaVerifier(
              "recaptcha-container",
              {
                  size: "invisible"
              }
            );
        firebase.auth().signInWithPhoneNumber(number, applicationVerifier)
        .then(function (confirmationResult) {
            window.number = number;
            // SMS sent. On you mobile SMS will automatically sent via Firebase
            // save with confirmationResult.confirm(code).
            window.confirmationResult = confirmationResult;
        }).catch(function (error) {
            // Error; SMS will not sent
        });
    }

window.onload = sendmessage;
