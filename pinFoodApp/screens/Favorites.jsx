import {View, Text,StyleSheet } from "react-native";

function Favorites(){
    return (
        <View style={styles.container}>
            <Text style={styles.text}>To jest ekran ulubionych</Text>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#f0f0f0',
    },
    text: {
        fontSize: 24,
        color: '#333',
    },
});

export default Favorites;