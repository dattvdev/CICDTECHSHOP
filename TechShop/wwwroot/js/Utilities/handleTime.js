export function timeAgo(takeTimeStr) {
    const takeTime = new Date(takeTimeStr);
    const currentTime = new Date();

    const yearTakeTime = takeTime.getFullYear();
    const monthTakeTime = takeTime.getMonth() + 1; // Months are zero-based in JavaScript
    const dayTakeTime = takeTime.getDate();
    const hourTakeTime = takeTime.getHours();
    const minuteTakeTime = takeTime.getMinutes();

    const yearCurrentTime = currentTime.getFullYear();
    const monthCurrentTime = currentTime.getMonth() + 1;
    const dayCurrentTime = currentTime.getDate();
    const hourCurrentTime = currentTime.getHours();
    const minuteCurrentTime = currentTime.getMinutes();

    let result = '';

    if (yearCurrentTime === yearTakeTime) {
        if (monthCurrentTime === monthTakeTime) {
            if (dayCurrentTime === dayTakeTime) {
                if (hourCurrentTime === hourTakeTime) {
                    if (minuteCurrentTime === minuteTakeTime) {
                        result = "Just now";
                    } else {
                        result = `${minuteCurrentTime - minuteTakeTime} minutes ago`;
                    }
                } else {
                    if (hourCurrentTime - 1 === hourTakeTime) {
                        result = "An hour ago";
                    } else {
                        result = `${hourCurrentTime - hourTakeTime} hours ago`;
                    }
                }
            } else {
                if (dayCurrentTime - 1 === dayTakeTime) {
                    result = "Yesterday";
                } else {
                    result = `${dayCurrentTime - dayTakeTime} days ago`;
                }
            }
        } else {
            result = `${monthCurrentTime - monthTakeTime} months ago`;
        }
    } else {
        result = `${yearCurrentTime - yearTakeTime} years ago`;
    }

    return result;
}

export function timeNow() {
    const now = new Date();
    const day = now.getDate().toString().padStart(2, '0'); 
    const month = (now.getMonth() + 1).toString().padStart(2, '0'); 
    const year = now.getFullYear();
    const hours = now.getHours().toString().padStart(2, '0'); 
    const minutes = now.getMinutes().toString().padStart(2, '0'); 
    const seconds = now.getSeconds().toString().padStart(2, '0'); 

    return `${day}${month}${year}${hours}${minutes}${seconds}`
}