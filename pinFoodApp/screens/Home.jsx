import React, {useEffect, useState} from 'react';
import {View} from "react-native";
import axios from "axios";
import {Text} from "react-native";
import {useAuth} from "../app/context/AuthContext";

function Home() {
    const [user, setUser] = useState({
        id: '',
        firstName: '',
        lastName: '',
        email: '',
    });

    useEffect(() => {
        const testCall = async () => {
            try {

                const response = await axios.get(`http://localhost:15000/api/users`);
                console.log(response.data);
                setUser(response.data);
            } catch (error) {
                console.error(error);
            }
        }

        testCall();
    }, []);

    return (
        <View>
            <Text>Home</Text>
            <Text>Welcome to the Home Screen!</Text>
            <Text>User Id: {user.id}</Text>
            <Text>First Name: {user.firstName}</Text>
            <Text>Last Name: {user.lastName}</Text>
            <Text>Email: {user.email}</Text>
        </View>
    );
}

export default Home;