import * as resources from "../../../resources/resources";

const baseAccountUrl = resources.urls.baseUrl + "api/account/";

export const accountUrls = {
    baseAccountUrl: baseAccountUrl,
    loginUrl: baseAccountUrl + "login",
    registrationUrl: baseAccountUrl + "register"
};

export const lables = {
    headers: {
        loginLabel: "Login",
        resitrationLabel: "Resitration"
    },

    properties: {
        fullNameLabel: "Full Name",
        emailLable: "E-mail",
        passwordLabel: "Password",
        remeberMeLabel: "Remember me ?"
    },

    commands: {
        signInLabel: "Sign In",
        signUpLabel: "Sign Up",
        registrLabel: "Registr",
        newToUsLabel: "New to us?",
        haveAccountLabel: "Have account?"
    },

    validationMessages: {
        passwordRequiredMessage: "Password is required.",
        passwordLengthMessage: "Password must be at least 6 characters long.",
        emailRequiredMessage: "E-mail is required",
        emailMessage: "Must be valid e-mail",
        fullNameRequiredMessage: "Full Name is required"
    }
};

export const popupMessages = {
    accountCreatedMessage: "Account was created.",
    accountCreationFailedMessage: "Error occure when registr user.",
    loginFailedMessage: "Email or password was incorrect. Try again."
};
