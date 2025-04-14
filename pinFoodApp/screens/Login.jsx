import React from 'react';
import {Button, TextInput, View, Text} from "react-native";
import {useAuth} from "../app/context/AuthContext";
import {StyleSheet} from "react-native";

function Login() {
    const [firstName, setFirstName] = React.useState('Test');
    const [lastName, setLastName] = React.useState('Test');
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    const {onLogin, onRegister} = useAuth();

    const handleLogin = () => {
        onLogin(email, password);
    }

    const handleRegister = () => {
        onRegister(firstName, lastName, email, password);
    }


    return (
        <View style={styles.container}>
            <TextInput
                placeholder="First Name"
                value={firstName}
                onChangeText={setFirstName}
            />
            <TextInput
                placeholder="Last Name"
                value={lastName}
                onChangeText={setLastName}
            />
            <TextInput
                placeholder="Email"
                value={email}
                onChangeText={setEmail}
            />
            <TextInput
                placeholder="Password"
                value={password}
                onChangeText={setPassword}
                secureTextEntry
            />
            <Button title="Login" onPress={handleLogin}/>
            <Button title="Register" onPress={handleRegister}/>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        padding: 16,
    },
    input: {
        height: 40,
        borderColor: 'gray',
        borderWidth: 1,
        marginBottom: 12,
        paddingHorizontal: 8,
    },
})

export default Login;