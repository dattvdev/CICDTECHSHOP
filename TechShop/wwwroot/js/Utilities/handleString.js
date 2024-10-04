export function insertSpaceBeforeUppercase(str) {
    return str.replace(/([a-z])([A-Z])/g, '$1 $2');
}