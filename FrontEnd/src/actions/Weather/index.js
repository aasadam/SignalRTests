import {GetWeather} from "./api" ;
import functions from '../../Util/functions'

export const handleGetWeather = async () => {
    return GetWeather()
            .then(response => {
                return response.data;
            });
};