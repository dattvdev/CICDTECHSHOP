export function randomColor() {
    const r = Math.floor(Math.random() * 256);
    const g = Math.floor(Math.random() * 256);
    const b = Math.floor(Math.random() * 256);

    const color = `#${r.toString(16).padStart(2, '0')}${g.toString(16).padStart(2, '0')}${b.toString(16).padStart(2, '0')}`;

    return color;
}

export function randomnColorWithSize(size) {
    let colors = []

    for (let i = 0; i < size; i = i + 1) {
        colors.push(randomColor())
    }

    return colors;
}
