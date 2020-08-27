import React from 'react';
import  Cookies  from 'universal-cookie';

const cookies = new Cookies();

const functions = {
    GetCookie: name => {
        return cookies.get(name);
    },

    SetCookie: (name, value) => {
        cookies.set(name,value);
    }
};

export default functions;