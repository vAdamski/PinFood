import {createContext, useContext, useEffect, useState} from 'react'
import axios from "axios";
import * as SecureStore from 'expo-secure-store';

const TOKEN_KEY = 'jwt_token';
export const API_URL = 'http://localhost:15000';
const AuthContext = createContext({});

export const useAuth = () => {
    return useContext(AuthContext);
}

export const AuthProvider = ({children}) => {
    const [authState, setAuthState] = useState({
        token: null,
        authenticated: null,
    });

    useEffect(() => {
        const loadToken = async () => {
            const token = await SecureStore.getItemAsync(TOKEN_KEY);
            if (token) {
                setAuthState({
                    token: token,
                    authenticated: true
                });
                axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            } else {
                setAuthState({
                    token: null,
                    authenticated: false
                });
            }
        }

        loadToken();
    }, []);

    const register = async (firstName, lastName, email, password) => {
        try {
            return await axios.post(`${API_URL}/api/users/register`, {
                firstName: firstName,
                lastName: lastName,
                email: email,
                password: password
            });
        } catch (e) {
            console.log(e);
        }
    }

    const login = async (email, password) => {
        try {

            const dto = {
                email: email,
                password: password
            }

            console.log(dto);

            const response = await axios.post(`${API_URL}/api/users/login`, dto, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            console.log(response.data);

            const token = response.data.token;

            setAuthState({
                token: token,
                authenticated: true
            });

            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;

            await SecureStore.setItemAsync(TOKEN_KEY, token);
        } catch (e) {
            if (e.response) {
                console.log('Response data:', e.response.data);
                console.log('Response status:', e.response.status);
                console.log('Response headers:', e.response.headers);
            } else {
                console.log('Error:', e.message);
            }
        }
    }

    const logout = async () => {
        setAuthState({
            token: null,
            authenticated: false
        });

        delete axios.defaults.headers.common['Authorization'];
        await SecureStore.deleteItemAsync(TOKEN_KEY);
    }

    const value = {
        onRegister: register,
        onLogin: login,
        onLogout: logout,
        authState
    };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
