import {Authenticate} from "./api" ;
import functions from '../../Util/functions'

export const handleAuthenticate = (username) => {
    return Authenticate(username)
            .then(response => {
                functions.SetCookie("TOKEN", response.data)
            });
};