
# Grand Circus Gains

A membership system for multiple fitness clubs

## Authors

- [@cr0schma](https://github.com/cr0schma)
- [@ashwathgowdanp](https://github.com/ashwathgowdanp)
- [@dgcngc01](https://github.com/dgcngc01)

## Project Structure

```shell
├── README.md
├── DataSources
│   ├── clubs.txt
│   ├── multiclubmembers.txt
│   └── singleclubmembers.txt
│
├── Classes
│   ├── Club
│   ├── DataAccess
│   ├── Members (abstract)
│   ├── MultiClubMember:Members
│   ├── Program
│   ├── SingleClubMember:Members
│   └── Validations
```

## App Operation

* You are greeted with the user login page. This page will accept a single club user or a multi club user.
* To invoke admin access you login as admin and use 123 for the password
* To return to the user login page from admin you select 0 from the main menu