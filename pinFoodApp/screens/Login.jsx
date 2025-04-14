import React from 'react';
import {Button, TextInput, View, Text, Switch} from "react-native";
import {useAuth} from "../app/context/AuthContext";
import {StyleSheet} from "react-native";

function Login() {
    const [isRegister, setIsRegister] = React.useState(false);
    const [firstName, setFirstName] = React.useState('');
    const [lastName, setLastName] = React.useState('');
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    const {onLogin, onRegister} = useAuth();

    const handleLogin = () => {
        onLogin(email, password);
    }

    const handleRegister = () => {
        onRegister(firstName, lastName, email, password);
    }

    const registerView = () => {
        return (
            <View style={styles.formContainer}>
                <TextInput
                    style={styles.input}
                    placeholder="First Name"
                    placeholderTextColor="#aaa"
                    value={firstName}
                    onChangeText={setFirstName}
                />
                <TextInput
                    style={styles.input}
                    placeholder="Last Name"
                    placeholderTextColor="#aaa"
                    value={lastName}
                    onChangeText={setLastName}
                />
                <TextInput
                    style={styles.input}
                    placeholder="Email"
                    placeholderTextColor="#aaa"
                    value={email}
                    onChangeText={setEmail}
                />
                <TextInput
                    style={styles.input}
                    placeholder="Password"
                    placeholderTextColor="#aaa"
                    value={password}
                    onChangeText={setPassword}
                    secureTextEntry
                />
                <Button
                    color="#6200ee"
                    title="Register"
                    onPress={handleRegister}
                />
            </View>
        )
    }

    const loginView = () => {
        return (
            <View style={styles.formContainer}>
                <TextInput
                    style={styles.input}
                    placeholder="Email"
                    placeholderTextColor="#aaa"
                    value={email}
                    onChangeText={setEmail}
                />
                <TextInput
                    style={styles.input}
                    placeholder="Password"
                    placeholderTextColor="#aaa"
                    value={password}
                    onChangeText={setPassword}
                    secureTextEntry
                />
                <Button
                    color="#6200ee"
                    title="Login"
                    onPress={handleLogin}
                />
            </View>
        )
    }

    return (
        <View style={styles.container}>
            <View style={styles.switchContainer}>
                <Text style={styles.switchText}>Logowanie</Text>
                <Switch
                    value={isRegister}
                    onValueChange={() => setIsRegister(!isRegister)}
                    ios_backgroundColor="#3e3e3e"
                    trackColor={{ false: "#767577", true: "#81b0ff" }}
                    thumbColor={isRegister ? "#6200ee" : "#f4f3f4"}
                />
                <Text style={styles.switchText}>Rejestracja</Text>
            </View>
            {isRegister ? registerView() : loginView()}
        </View>
    );
}

export default Login;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        padding: 20,
        backgroundColor: '#f2f2f2'
    },
    switchContainer: {
        flexDirection: 'row', // Ustawienie wiersza
        alignItems: 'center', // Wycentrowanie elementów w pionie
        marginBottom: 20, // Odstęp od formularza
    },
    switchText: {
        fontSize: 18,
        fontWeight: 'bold',
        marginHorizontal: 10, // Odstęp od przełącznika
        color: '#333'
    },
    formContainer: {
        width: '100%',
        padding: 10,
        marginVertical: 20,
        backgroundColor: '#fff',
        borderRadius: 10,
        elevation: 4, // cień dla Androida
        shadowColor: '#000', // cień dla iOS
        shadowOffset: { width: 0, height: 2 }, // cień dla iOS
        shadowOpacity: 0.2, // cień dla iOS
        shadowRadius: 4, // cień dla iOS
    },
    input: {
        height: 50,
        borderColor: '#ccc',
        borderWidth: 1,
        borderRadius: 5,
        marginBottom: 10,
        paddingHorizontal: 10,
        backgroundColor: '#f9f9f9'
    }
});