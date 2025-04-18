import {View, Text, StyleSheet, Button} from 'react-native';
import {useAuth} from "../app/context/AuthContext";

function Profile() {
    const {onLogout} = useAuth();

    return (
        <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
            <Text>To jest ekran profilu</Text>
            <Button title="Wyloguj się" onPress={onLogout} />
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center'
    }
});

export default Profile;
