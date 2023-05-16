echo off
title Sign In to AltspaceVR
curl -v -d "user[email]=kk.penaranda@uniandes.edu.co&user[password]=kelly19 .." https://account.altvr.com/users/sign_in.json -c cookie
