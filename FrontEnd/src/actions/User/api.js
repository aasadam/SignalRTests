import API from "../../Util/API";

//Autenticação
export const Authenticate = (username) =>
  API.post("root/login", { username });