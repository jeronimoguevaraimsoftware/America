var loadStep1OP = function () {
    $('#step1').find('input').each(function () {
        $(this).prop("disabled", true);
    });

    let checkedProductionEquipmentIsAvailable = $("#ProductionEquipmentIsAvailable").val()
    if (checkedProductionEquipmentIsAvailable === "True") {
        $("#ProductionEquipmentCheckTrue").prop("checked", true)
        $("#ProductionEquipmentCheckFalse").prop("checked", false)
    }
    else if (checkedProductionEquipmentIsAvailable === "False") {
        $("#ProductionEquipmentCheckTrue").prop("checked", false)
        $("#ProductionEquipmentCheckFalse").prop("checked", true)
    }

    let reviewedBy = $("#ProductionEquipmentReviewedBy").val()
    let reviewedDateString = $("#ProductionEquipmentReviewedDate").val()
    let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
    $('#ProductionEquipmentReviewedSignature').html(getSignature(reviewedBy, reviewedDate))

    $("#table-2").find("tbody tr").each(function (index) {
        let itemId = $(this).find("td").first().text()
        let checked = $(this).find("#MonitoringEquipmentIsCalibrated" + itemId).val()

        if (checked === "True") {
            $(this).find("#MonitoringEquipmentCheckTrue" + itemId).prop("checked", true)
            $(this).find("#MonitoringEquipmentCheckFalse" + itemId).prop("checked", false)
            $('#table-2').removeAttr('hidden');
        }
        else if (checked === "False") {
            $(this).find("#MonitoringEquipmentCheckTrue" + itemId).prop("checked", false)
            $(this).find("#MonitoringEquipmentCheckFalse" + itemId).prop("checked", true)
            $('#table-2').prop('hidden', true);
        }

        let reviewedBy = $(this).find("#MonitoringEquipmentReviewedBy" + itemId).val()
        let reviewedDateString = $(this).find("#MonitoringEquipmentReviewedDate" + itemId).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#MonitoringEquipmentReviewedSignature" + itemId).html(getSignature(reviewedBy, reviewedDate))
    });
}

var loadStep2OP = function () {
    $('#step2').find('input').each(function () {
        $(this).prop("disabled", true);
    });

    let checkedPipelineClearanceInCompliance = $("#PipelineClearanceInCompliance").val()
    if (checkedPipelineClearanceInCompliance === "True") {
        $("#PipelineClearanceCheckTrue").prop("checked", true)
        $("#PipelineClearanceCheckFalse").prop("checked", false)

        $('#table-4').show()
    }
    else if (checkedPipelineClearanceInCompliance === "False") {
        $("#PipelineClearanceCheckTrue").prop("checked", false)
        $("#PipelineClearanceCheckFalse").prop("checked", true)

        $('#table-4').hide()
    }

    let reviewedBy = $("#PipelineClearanceReviewedBy").val()
    let reviewedDateString = $("#PipelineClearanceReviewedDate").val()
    let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
    $('#PipelineClearanceReviewedSignature').html(getSignature(reviewedBy, reviewedDate))

    let productionStartedDateString = $("#PipelineClearanceProductionStartedDate").val()
    let productionStartedDate = !isNullOrEmpty(productionStartedDateString) ? new Date(productionStartedDateString) : null
    if (productionStartedDate !== null) {
        $('#TxtDateProduccionLot').val(getDate(productionStartedDate))
        $('#TxtDateHProduccionLot').val(getDateTime(productionStartedDate))
    }
    let productionEndDateString = $("#PipelineClearanceProductionEndDate").val()
    let productionEndDate = !isNullOrEmpty(productionEndDateString) ? new Date(productionEndDateString) : null
    if (productionEndDate !== null) {
        $('#TxtDateProduccionLotEnd').val(getDate(productionEndDate))
        $('#TxtDateHProduccionLotEnd').val(getDateTime(productionEndDate))
    }
}

var loadStep3OP = function () {
    $('#step3').find('input').each(function () {
        $(this).prop("disabled", true);
    });

    $("#table-5").find("tbody tr").each(function (index) {
        let itemId = $(this).find("td").first().text()
        let checked = $(this).find("#ControlVariablesInCompliance" + itemId).val()

        if (checked === "True") {
            $(this).find("#ControlVariablesCheckTrue" + itemId).prop("checked", true)
            $(this).find("#ControlVariablesCheckFalse" + itemId).prop("checked", false)
        }
        else if (checked === "False") {
            $(this).find("#ControlVariablesCheckTrue" + itemId).prop("checked", false)
            $(this).find("#ControlVariablesCheckFalse" + itemId).prop("checked", true)
        }

        let reviewedBy = $(this).find("#ControlVariablesReviewedBy" + itemId).val()
        let reviewedDateString = $(this).find("#ControlVariablesReviewedDate" + itemId).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#ControlVariablesReviewedSignature" + itemId).html(getSignature(reviewedBy, reviewedDate))
    });

    $("#table-6").find("tbody tr").each(function (index) {
        let itemId = $(this).find("td").first().text()
        let checked = $(this).find("#CriticalParametersInCompliance" + itemId).val()

        if (checked === "True") {
            $(this).find("#CriticalParametersCheckTrue" + itemId).prop("checked", true)
            $(this).find("#CriticalParametersCheckFalse" + itemId).prop("checked", false)
        }
        else if (checked === "False") {
            $(this).find("#CriticalParametersCheckTrue" + itemId).prop("checked", false)
            $(this).find("#CriticalParametersCheckFalse" + itemId).prop("checked", true)
        }

        let reviewedBy = $(this).find("#CriticalParametersReviewedBy" + itemId).val()
        let reviewedDateString = $(this).find("#CriticalParametersReviewedDate" + itemId).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#CriticalParametersReviewedSignature" + itemId).html(getSignature(reviewedBy, reviewedDate))
    });
}

var loadStep4OP = function () {
    $('#step4').find('input').each(function () {
        $(this).prop("disabled", true);
    });

    $("#table-7").find("tbody tr").each(function (index) {
        let itemId = $(this).find("td").first().text()
        let checked = $(this).find("#CriticalQualityAttributesInCompliance" + itemId).val()

        if (checked === "True") {
            $(this).find("#CriticalQualityAttributesCheckTrue" + itemId).prop("checked", true)
            $(this).find("#CriticalQualityAttributesCheckFalse" + itemId).prop("checked", false)
        }
        else if (checked === "False") {
            $(this).find("#CriticalQualityAttributesCheckTrue" + itemId).prop("checked", false)
            $(this).find("#CriticalQualityAttributesCheckFalse" + itemId).prop("checked", true)
        }

        let reviewedBy = $(this).find("#CriticalQualityAttributesReviewedBy" + itemId).val()
        let reviewedDateString = $(this).find("#CriticalQualityAttributesReviewedDate" + itemId).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#CriticalQualityAttributesReviewedSignature" + itemId).html(getSignature(reviewedBy, reviewedDate))
    });
}

var loadStep5OP = function () {
    $('#step5').find('input').each(function () {
        $(this).prop("disabled", true);
    });

    let checkedBatchDetailsReleasedBy = $("#BatchDetailsIsReleased").val()
    if (checkedBatchDetailsReleasedBy === "True") {
        $("#BatchDetailsReleasedCheckTrue").prop("checked", true)
        $("#BatchDetailsReleasedCheckFalse").prop("checked", false)
    }
    else if (checkedBatchDetailsReleasedBy === "False") {
        $("#BatchDetailsReleasedCheckTrue").prop("checked", false)
        $("#BatchDetailsReleasedCheckFalse").prop("checked", true)
    }

    let releasedBy = $("#BatchDetailsReleasedBy").val()
    let releasedDateString = $("#BatchDetailsReleasedDate").val()
    let releasedDate = !isNullOrEmpty(releasedDateString) ? new Date(releasedDateString) : null
    $('#BatchDetailsReleasedSignature').html(getSignature(releasedBy, releasedDate))
}

var loadStep6OP = function () {
    $("#OA").show();

    $('#step6').find('input').each(function () {
        $(this).prop("disabled", true);
    });

    let checkedReleasedBy = $("#IsReleased").val()
    if (checkedReleasedBy === "True") {
        $("#ReleasedCheckTrue").prop("checked", true)
        $("#ReleasedCheckFalse").prop("checked", false)
    }
    else if (checkedReleasedBy === "False") {
        $("#ReleasedCheckTrue").prop("checked", false)
        $("#ReleasedCheckFalse").prop("checked", true)
    }

    let releasedBy = $("#ReleasedBy").val()
    let releasedDateString = $("#ReleasedDate").val()
    let releasedDate = !isNullOrEmpty(releasedDateString) ? new Date(releasedDateString) : null
    $('#ReleasedSignature').html(getSignature(releasedBy, releasedDate))
}

var showPipelineClearanceHistoryOP = function () {
    //Despeje de línea historial
    $.ajax({
        type: "GET",
        url: "/ProductionOrder/GetDetailDL/?Id=" + $("#Id").val(),
        success: function (partialViewResult) {
            $("#table3-detailInfo").html(partialViewResult);
            $('#table3-detailInfo').prop('hidden', false);

            // load signature and second notes
            $("#table-3Detail").find("tbody tr").each(function (index) {
                let reviewedBy = $(this).find("#DeviationReportReviewedBySecond" + index).val()
                let reviewedDateString = $(this).find("#DeviationReportReviewedDateSecond" + index).val()
                let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
                $(this).find("#DeviationReportReviewedSecondSignature" + index).html(getSignature(reviewedBy, reviewedDate))
            });

            $("#table-3Detail").find(".sign-deviation").each(function (index) {
                $(this).remove()
            })
        }
    });
}

var hidePipelineClearanceHistoryOP = function () {
    $('#table3-detailInfo').prop('hidden', true);
}