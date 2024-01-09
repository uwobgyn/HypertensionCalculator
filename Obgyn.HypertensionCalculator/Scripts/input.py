#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Thu Jul 29 17:32:37 2021

@author: Troy-T
"""
import warnings
warnings.filterwarnings("ignore")
# from sklearn.externals import joblib
import joblib
import pandas as pd
import sys

def predict(data):
    column_names = ['bp_high24', 'bp_low24', 'bp_high48', 'bp_low48',
       'm9\'s bmi_prepregnancy',
       'gestational_age (ob_clinician\'s_final estimate-_calculated)',
       'maternal_age', 'bp_high', 'bp_low', 'If Ibuprofen',
       'If nifedipine acute', 'If nifedipine sustain', 'If Labetalol given IV',
       'If Labetalol given PO', 'If hydralazine']
    df = pd.DataFrame(columns = column_names)
    df.loc[0] = data
    # this is what is making it have issues
    joblib_RF_model = joblib.load('C:\\OrchardCore\\src\\Obgyn.DEV\\Obgyn.Modules.Cms\\Obgyn.HypertensionCalculator\\Scripts\\model.joblib')
    y_predRF = joblib_RF_model.predict_proba(df).T[1]
    return y_predRF[0]
    # return 7

if __name__=="__main__":
    # print("Enter data seperated by space, in the order of:")
    # print("bp_high24\nbp_low24\nbp_high48\nbp_low48\nm9's bmi_prepregnancy\ngestational_age (ob_clinician's_final estimate-_calculated)\nmaternal_age\nbp_high\nbp_low\nIf Ibuprofen\nIf nifedipine acute\nIf nifedipine sustain\nIf Labetalol given IV\nIf Labetalol given PO\nIf hydralazine")
    # print("The last six arguments needs to be binary variable with 1 for yes and 0 for no")
    # data = input().split(" ")
    # data = [float(x) for x in data]
    # predict(data)

    # print("Continue with original args?")
    sys.argv.pop(0)
    print(predict(sys.argv))
