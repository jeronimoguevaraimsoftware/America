function getSignature(name, date) {
    if (date === null) {
        return "<span>" + name + "</span>"
    }
    else {
        return "<span>" + name + "</span><br /><span>" + getDate(date) + "</span><br /><span>" + getDateTime(date) + "</span>"
    }
}

function isNullOrEmpty(value) {
    return value === null || value === undefined || value === ''
}

function getDateString(date) {
    return getDate(date) + 'T' + getDateTime(date)
}

function getDate(date) {
    if (date === null) {
        return "<span>" + "no info" + "</span>"
    }
    else {
        let mm = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)
        let dd = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
        let yy = date.getFullYear()

        return yy + '-' + mm + '-' + dd
    }
}

function getDateTime(date) {
    if (date === null) {
        return "<span>" + "no info" + "</span>"
    }
    else {
        let minutes = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes()
        let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours()
        let time = hours + ":" + minutes

        return time
    }
}

function getCheckboxPairValue(elementTrue, elementFalse) {
    if (elementTrue.prop("checked")) {
        return true
    }

    if (elementFalse.prop("checked")) {
        return false
    }

    return null
}